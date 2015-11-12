namespace ScriptParser
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

    let getNestedFunctions (tokens : Token list) = // TODO : If this is fixed then parser should work.
        ([ERROR("left")],[ERROR("right")])

    let rec (|MultiSelector|_|) (tokens : Token list) = 
        match getNestedFunctions tokens with
        | ([], car::cdr) -> Some(Error("Left function is not recognised"))
        | (car::cdr, []) -> Some(Error("Right function is not recognised"))
        | ([], []) -> Some(Error("Multi functions does not contain any functions"))
        | (left, right) -> Some(Multi(parser left, parser right))
    
    and parser (tokens : Token list) =
        match tokens with
        | TypeSelector t -> t
        | FolderSelector f -> f
        | PropertySelector p -> p
        | MultiSelector m -> m
        | ErrorSelector e -> e
        | _ -> Error("Syntax error. Program matches no known structure.")

    and Parse (input : string) : Program =
        let tokens = Lexer.getTokens input
        parser tokens