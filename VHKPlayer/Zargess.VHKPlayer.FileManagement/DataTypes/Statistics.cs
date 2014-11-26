using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.DataTypes {
    public class Statistics {
        public int Goals { get; set; }
        public int Shots { get; set; }
        public int Saves { get; set; }
        public int SaveAttempts { get; set; }
        public int YellowCard { get; set; }
        public int Suspension { get; set; }
        public int RedCard { get; set; }

        public Statistics() : this(0,0,0,0,0,0,0) { }

        private Statistics(int g, int s, int sa, int savea, int y, int sus, int r) {
            Goals = g;
            Shots = s;
            Saves = sa;
            SaveAttempts = savea;
            YellowCard = y;
            Suspension = sus;
            RedCard = r;
        }

        public Statistics Clone() {
            return new Statistics(Goals, Shots, Saves, SaveAttempts, YellowCard, Suspension, RedCard);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Statistics)obj);
        }

        public bool Equals(Statistics other) {
            return Goals == other.Goals &&
                Shots == other.Shots &&
                Saves == other.Saves &&
                SaveAttempts == other.SaveAttempts &&
                YellowCard == other.YellowCard &&
                Suspension == other.Suspension &&
                RedCard == other.RedCard;
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = Goals;
                hashCode = (hashCode * 397) ^ Shots;
                hashCode = (hashCode * 397) ^ Saves;
                hashCode = (hashCode * 397) ^ SaveAttempts;
                hashCode = (hashCode * 397) ^ YellowCard;
                hashCode = (hashCode * 397) ^ Suspension;
                hashCode = (hashCode * 397) ^ RedCard;
                return hashCode;
            }
        }
    }
}