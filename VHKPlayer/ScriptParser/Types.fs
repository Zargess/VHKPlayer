namespace ScriptParser
    open System
    type Token = | LPAREN
               | RPAREN
               | COLON
               | MULTI
               | PROPERTY
               | TYPE
               | FOLDER
               | NAME
               | VALUE
               | PATH
               | LEFT
               | RIGHT
               | STRING of string
               | INT of Int32
               | BOOLEAN of bool
               | ID of string
               | ERROR of string
               | END

    type Program = | Folder of string
                 | Type of string
                 | Property of string * Object
                 | Multi of Program * Program
                 | Error of string
