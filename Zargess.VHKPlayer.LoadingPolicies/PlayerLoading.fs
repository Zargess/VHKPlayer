namespace Zargess.VHKPlayer.LoadingPolicies

module PlayerLoading =
    open System
    open System.IO
    open Utiliti
    open FolderLoading

    type File = { Name:string; Path:string; }
    type StatFiles = { Music:File; Video:File; Picture:File; }
    type FileSet = { Picture:File; Video:File; Stats:StatFiles; }
    type Player = { Number:int; Name:string; Picture:File; Video:File; StatFiles:StatFiles; Trainer:bool }

    let supportedTypes = 
        let pics = settingList "supportedPicture"
        let vids = settingList "supportedVideo"
        let music = settingList "supportedMusic"
        List.concat [ pics; vids; music ]

    let emptyFile = { Name=""; Path="";}
    let emptyStats = { Music=emptyFile; Video=emptyFile; Picture=emptyFile; }

    let (==) (a : File) (b : File) =
        a.Path = b.Path

    let (===) (a : StatFiles) (b : StatFiles) =
        let mus = a.Music == b.Music
        let vid = a.Video == b.Video
        let pic = a.Picture == b.Picture
        mus && vid && pic

    let isSupported (file : File) =
        List.map (fun x -> file.Name.EndsWith x) supportedTypes
        |> List.fold (fun a b -> a || b) false

    let constructFile path = 
        let name = folderName path
        { Name=name; Path=path; }

    let FileNameWithoutExtension (file : File) =
        file.Name.Split[|'.'|]
        |> (fun arr -> arr.[0])

    let createPlayer (file : FileSet) =
        let number = int(file.Picture.Name.[0..2])
        let name = file.Picture.Name.[6..(file.Picture.Name.Length - 5)]
        let res = { 
            Number=number; 
            Name=name; 
            Picture=file.Picture;
            Video=file.Video;
            StatFiles=file.Stats
            Trainer = number >= 90 && file.Stats === emptyStats && file.Video == emptyFile
        }
        res

    let FileList source =
        Directory.EnumerateFiles source
        |> List.ofSeq
        |> List.map (fun x -> constructFile x)
        |> List.filter isSupported

    let getFileName (f : File) =
        f.Name
    
    // TODO : Make this code cleaner
    let createAllPlayers source =
        let spiller = FileList (source + "\\Spiller")
        let video = FileList (source + "\\SpillerVideo")
        let statsPic = FileList (source + "\\SpillerVideoStat") 
        let statsMus = FileList (source + "\\SpillerVideoStat\\mp3")
        let statsVid = FileList (source + "\\SpillerVideoStat\\Video")
        let stats =
            List.map (fun x -> 
                        let pic = x
                        let q = FileNameWithoutExtension pic
                        let mus = find getFileName emptyFile (q + ".mp3") statsMus
                        let vid = find getFileName emptyFile (q + ".avi") statsVid
                        { Music=mus; Video=vid; Picture=pic; }
                        ) statsPic
        let filesets =
            List.map (fun x -> 
                        let pic : File = x
                        let q = FileNameWithoutExtension pic
                        let vid = find getFileName emptyFile (q + ".avi") video
                        let stat = find (fun (x : StatFiles) -> x.Picture.Name) emptyStats pic.Name stats
                        { Picture=pic; Video=vid; Stats=stat }) spiller
        List.map createPlayer filesets