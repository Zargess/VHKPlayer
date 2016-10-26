using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHKPlayer.Models;
using VHKPlayer.Queries.GetBoolSetting;
using VHKPlayer.Queries.GetDoubleSetting;
using VHKPlayer.Queries.Interfaces;
using VHKPlayer.Utility;

namespace VHKPlayer.Queries.GetWindowPosition
{
    class GetWindowPositionQueryHandler : IQueryHandler<GetWindowPositionQuery, WindowPosition>
    {
        private readonly IQueryProcessor _processor;

        public GetWindowPositionQueryHandler(IQueryProcessor processor)
        {
            _processor = processor;
        }

        public WindowPosition Handle(GetWindowPositionQuery query)
        {
            return new WindowPosition
            {
                Top = _processor.Process(new GetDoubleSettingQuery
                {
                    SettingName = Constants.TopSettingName
                }),
                Left = _processor.Process(new GetDoubleSettingQuery
                {
                    SettingName = Constants.LeftSettingName
                }),
                Height = _processor.Process(new GetDoubleSettingQuery
                {
                    SettingName = Constants.HeightSettingName
                }),
                Width = _processor.Process(new GetDoubleSettingQuery
                {
                    SettingName = Constants.WidthSettingName
                }),
                Maximized = _processor.Process(new GetBoolSettingQuery
                {
                    SettingName = Constants.MaximizedSettingName
                })
            };
        }
    }
}
