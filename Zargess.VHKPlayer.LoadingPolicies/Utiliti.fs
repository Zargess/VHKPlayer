module Utiliti

open System

let Compare (a : string) (b : string) (case:StringComparison) = String.Compare(a, b, case) = 0

let rec find func empty (element : string) list =
    match list with
    | [] -> empty
    | first::rest ->
        if Compare ((func first) :string) element StringComparison.OrdinalIgnoreCase then
            first
        else find func empty element rest

let rec lastElement list = 
    match list with
    | hd::[] -> hd
    | hd::rest -> lastElement rest
    | [] -> failwith "empty"