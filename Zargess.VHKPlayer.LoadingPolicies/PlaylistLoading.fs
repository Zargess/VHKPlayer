namespace Zargess.VHKPlayer.LoadingPolicies

module PlaylistLoading =
    open System
    open System.IO
    open Utiliti
    open FolderLoading
    open PlayerLoading

    type Playlist = { Name : string; Content : File list; FirstOnly : bool; mutable Repeat : bool; }

    let getFiles source =
        Directory.GetFiles(source)
        |> Array.toList
        |> List.map constructFile
        |> List.filter isSupported

    let createPlaylist name content firstonly repeat = 
        { Name = name; Content = content; FirstOnly = firstonly; Repeat = repeat }

    let playlistFromFolderContent source firstonly repeat =
        let files = getFiles source
        let name = folderName source
        createPlaylist name files firstonly repeat

    let getNumber (file : File) index = 
        try
            int(file.Name.[index - 1]) - 48
        with
            | :? FormatException as ex ->
                0

    let sortedPlaylist source name index =
        let files = List.sortBy (fun x -> getNumber x index) (List.filter (fun x -> (getNumber x index) <> 0) (getFiles source))                    
        let content = files
        createPlaylist name files false false

    // test
    let rek = sortedPlaylist @"C:\Users\MFH\vhk\Rek" "RekFørKamp" 1
    let tensek = playlistFromFolderContent @"C:\Users\MFH\vhk\10sek" false false