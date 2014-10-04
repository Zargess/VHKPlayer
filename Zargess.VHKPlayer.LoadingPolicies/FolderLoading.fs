namespace Zargess.VHKPlayer.LoadingPolicies

module FolderLoading =
    open System.IO
    open Utiliti
    open System.Collections.Generic;

    let folderName (path:string) =
        lastElement (List.ofSeq (path.Split[|'\\'|]))

    let shouldInclude limits dir =
        List.fold (fun a b -> a && b) true (List.map (fun x -> not ( Compare x (folderName dir))) limits)

    let rec generalfolderfunc func limits root =
        match root with
        | [] -> []
        | first::rest ->
            match shouldInclude limits first with 
            | true ->
                let current =
                    func first
                    |> generalfolderfunc func limits
                let other =
                    rest
                    |> generalfolderfunc func limits
                List.concat [[first]; current; other;]
            | false ->
                rest |> generalfolderfunc func limits

    let rec enumerableToList (e : IEnumerator<_> ) list  =
        match e.MoveNext() with
        | false -> list
        | true ->
            e.Current :: list
            |> enumerableToList e

    let findFolders root = 
        let e = (Directory.EnumerateDirectories root).GetEnumerator()
        enumerableToList e []

    let getAllFolders target = generalfolderfunc findFolders [] [target]
    let getSomeFolders target (limits : list<string>) = generalfolderfunc findFolders limits [target]