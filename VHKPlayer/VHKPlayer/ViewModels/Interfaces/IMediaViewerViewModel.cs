using System.ComponentModel;
using System.Windows;

namespace VHKPlayer.ViewModels.Interfaces
{
    public interface IMediaViewerViewModel : INotifyPropertyChanged
    {
        bool StatsEnabled { get; set; }
        bool SoundEnabled { get; set; }
        bool FullScreen { get; set; }
        int Screen { get; set; }
        Thickness SavingPlacement { get; set; }
        Thickness ScoringPlacement { get; set; }
        Thickness PenaltyPlacement { get; set; }
        Thickness TextBlockPlacement { get; set; }
        double TextSize { get; set; }
    }
}