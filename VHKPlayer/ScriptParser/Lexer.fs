namespace ScriptParser
    open System.Text.RegularExpressions

    exception LexerException of string
    module Lexer =
        let (|Prefix|_|) (p:string) (s:string) =
            if s.StartsWith(p) then
                Some(s.Substring(p.Length))
            else
                None

        let (|Integer|_|) (p:string) =
            let test = p.Split(')').[0]
            let mutable intvalue = 0
            if System.Int32.TryParse(test, &intvalue) then Some(intvalue)
            else None

        let rec getNextString (input : string) (res : string) : string * string =
            match input with
            | "" -> raise (LexerException "Lexer error. String not closed")
            | Prefix "\"" rest -> (res, rest)
            | _ ->
                let char = input.[0].ToString()
                let newInput = input.Remove(0, 1)
                let newRes = res + char
                getNextString newInput newRes

        let findIdentifier (input : string) : string =
            let identifier = Regex.Match(input, "^[_a-zA-Z][_a-zA-Z]*")
            match identifier.Success with
            | true -> identifier.Value
            | false -> ""
            
        let rec getNextToken (input : string) : Token * string =
                match input with
                | "" -> (END, "")
                | Prefix "\"" rest -> 
                    try
                        let (res, cdr) = getNextString rest ""
                        (STRING(res), cdr)
                    with
                    | LexerException msg -> (ERROR(msg), rest)
                | Prefix " " rest -> getNextToken rest
                | Prefix "(" rest -> (LPAREN, rest)
                | Prefix ")" rest -> (RPAREN, rest)
                | Prefix ":" rest -> (COLON, rest)
                | Prefix "multi" rest -> (MULTI, rest)
                | Prefix "property" rest -> (PROPERTY, rest)
                | Prefix "type" rest -> (TYPE, rest)
                | Prefix "folder" rest -> (FOLDER, rest)
                | Prefix "name" rest -> (NAME, rest)
                | Prefix "value" rest -> (VALUE, rest)
                | Prefix "path" rest -> (PATH, rest)
                | Prefix "left" rest -> (LEFT, rest)
                | Prefix "right" rest -> (RIGHT, rest)
                | Prefix "True" rest -> (BOOLEAN(true), rest)
                | Prefix "False" rest -> (BOOLEAN(false), rest)
                | Integer i ->
                    let x = i.ToString()
                    let rest = input.Remove(0, x.Length)
                    (INT(i), rest)
                | _ ->
                    let x = findIdentifier input
                    if (x = "")
                    then (ERROR("Symbol(s) not recognised:" + input), input)
                    else
                        let rest = input.Remove(0, x.Length) 
                        (ID(x), rest)

        let getTokens (input : string) : Token list = 
            let rec constructList (input : string) resList =
                match getNextToken input with
                | (END, "") -> END::resList
                | (token, rest) -> constructList rest (token::resList)
            constructList input []
            |> List.rev

