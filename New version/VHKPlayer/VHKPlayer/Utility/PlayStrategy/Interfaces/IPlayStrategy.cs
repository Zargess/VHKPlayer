using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;

namespace VHKPlayer.Utility.PlayStrategy.Interfaces
{
    public interface IPlayStrategy
    {
        void Play(ICollection content);
        FileNode PeekNext();
    }
}
