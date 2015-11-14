namespace ScriptParser

exception SyntaxException of string
module Parser = 
   
    let rec findError (tokens : Token list) =
        match tokens with
        | [] -> None
        | ERROR(msg)::cdr -> Some(Error(msg))
        | _::cdr -> findError(cdr)

    let (|FolderSelector|_|) (tokens : Token list) =
        match tokens with
        | [LPAREN; FOLDER; PATH; COLON; STRING(s); RPAREN; END] -> Some(Folder(s))
        | _ -> None

    let (|TypeSelector|_|) (tokens : Token list) =
        match tokens with
        | [LPAREN;TYPE;NAME;COLON;ID(id);RPAREN;END] -> Some(Type(id))
        | _ -> None

    let (|PropertySelector|_|) (tokens : Token list) =
        match tokens with
        | [LPAREN; PROPERTY; NAME; COLON; ID(id); VALUE; COLON; STRING(s); RPAREN; END] -> Some(Property(id, s))
        | [LPAREN; PROPERTY; NAME; COLON; ID(id); VALUE; COLON; INT(i); RPAREN; END] -> Some(Property(id, i))
        | [LPAREN; PROPERTY; NAME; COLON; ID(id); VALUE; COLON; BOOLEAN(b); RPAREN; END] -> Some(Property(id, b))
        | _ -> None

    let (|ErrorSelector|_|) (tokens : Token list) =
        findError(tokens)

    let rec removeFirstXItems list counter =
        match list with
        | [] when counter = 0 -> list
        | [] when counter > 0 -> raise (SyntaxException "Counter is higher than list length")
        | x when counter = 0 -> x
        | car::cdr when counter > 0 -> removeFirstXItems cdr (counter - 1)
        | x when counter < 0 -> raise (SyntaxException "Counter must not be negative")
        | _ -> raise (SyntaxException "dunno")

    let getNestedFunctions (tokens : Token list) : Token list * Token list =
        let rec findFunction (tokens : Token list) (nestingLevel : int) (res : Token list) =
            match tokens with
            | [] -> ([], [])
            | LPAREN::MULTI::cdr -> findFunction cdr (nestingLevel + 1) (res@[LPAREN;MULTI])
            | LPAREN::cdr -> findFunction cdr (nestingLevel + 1) (res@[LPAREN])
            | RPAREN::cdr when nestingLevel = 1 -> (res@[RPAREN], cdr)
            | RPAREN::cdr when nestingLevel > 1 -> findFunction cdr (nestingLevel - 1) (res@[RPAREN])
            | car::cdr -> findFunction cdr nestingLevel (res@[car])
        try
            let (left, rest) = findFunction (removeFirstXItems tokens 4) 0 []
            printfn "%A" left |> ignore 
            let (right, _) = findFunction (removeFirstXItems rest 2) 0 []
            printfn "%A" right |> ignore
            (left@[END], right@[END])
        with
        | SyntaxException msg -> ([ERROR(msg)], [ERROR(msg)])
    
    let rec parser (tokens : Token list) =
        match tokens with
        | TypeSelector t -> t
        | FolderSelector f -> f
        | PropertySelector p -> p
        | LPAREN::MULTI::cdr -> 
            let (left, right) = getNestedFunctions tokens
            Multi(parser left, parser right)
        | ErrorSelector e -> e
        | _ -> Error("Syntax error. Program matches no known structure.")

    let Parse (input : string) : Program =
        let tokens = Lexer.getTokens input
        parser tokens

