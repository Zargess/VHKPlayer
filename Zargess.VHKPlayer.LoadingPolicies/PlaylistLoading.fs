namespace Zargess.VHKPlayer.LoadingPolicies

module PlaylistLoading =
    open System
    open System.IO
    open FolderLoading
    open PlayerLoading

    type Playlist = { Name : string; Content : File list; }

    let getFiles source =
        Directory.GetFiles(source)
        |> Array.toList
        |> List.map constructFile
        |> List.filter isSupported

    let createPlaylist name content = 
        { Name = name; Content = content; }

    // Creates a playlist of files which lies in a given source. it takes all files that are supported
    let playlistFromFolderContent source =
        let files = getFiles source
        let name = folderName source
        createPlaylist name files

    let getNumber (file : File) index = 
        try
            int(file.Name.[index - 1]) - 48
        with
            | :? FormatException as ex -> 0

    // Creates a playlist of files in the given source where the char in position number index is a number and not 0. It also sorts by that number
    let sortedPlaylist source name index =
        let files = List.sortBy (fun x -> getNumber x index) (List.filter (fun x -> (getNumber x index) <> 0) (getFiles source))                    
        createPlaylist name files

    // test
    let rek = sortedPlaylist @"C:\Users\MFH\vhk\Rek" "RekFørKamp" 1
    let tensek = playlistFromFolderContent @"C:\Users\MFH\vhk\10sek"