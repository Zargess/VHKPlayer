namespace Zargess.VHKPlayer.LoadingPolicies

module FolderLoading =
    open System.IO
    open Utiliti

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

    let findFolders root = Array.toList (Directory.GetDirectories root)
    let getAllFolders target = generalfolderfunc findFolders [] [target]
    let getSomeFolders target (limits : list<string>) = generalfolderfunc findFolders limits [target]