// CivOne
//
// To the extent possible under law, the person who associated CC0 with
// CivOne has waived all copyright and related or neighboring rights
// to CivOne.
//
// You should have received a copy of the CC0 legalcode along with this
// work. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.

using System;
using System.Drawing;
using System.Linq;
using CivOne.Enums;
using CivOne.Events;
using CivOne.Interfaces;
using CivOne.GFX;
using CivOne.Templates;
using CivOne.Units;

namespace CivOne.Screens
{
	internal class UnitStack : BaseScreen
	{
		private const int XX = 101;
		private const int WIDTH = 121;

		private readonly int _x, _y;
		private readonly IUnit[] _units;

		private bool _update = true;
		
		public override bool HasUpdate(uint gameTick)
		{
			if (_update)
			{
				if (!_units.Any())
				{
					// No units, close the dialog
					HandleClose();
					Destroy();
					return true;
				}

				int height = (16 * _units.Length) + 6;
				int yy = (200 - height) / 2;

				Picture dialog = new Picture(WIDTH + (4 - (WIDTH % 4)), height);
				dialog.FillRectangle(3, 1, 1, WIDTH - 2, height - 2);
				dialog.AddBorder(15, 8, 0, 0, WIDTH, height);

				for (int i = 0; i < _units.Length; i++)
				{
					IUnit unit = _units[i];
					dialog.AddLayer(unit.GetUnit(unit.Owner).Image, 4, (i * 16) + 3);
					dialog.DrawText(unit.Name + (unit.Veteran ? " (V)" : ""), 0, 15, 27, (i * 16) + 4);
					dialog.DrawText(unit.Home == null ? "NONE" : unit.Home.Name, 0, 14, 27, (i * 16) + 12);
				}

				_canvas.FillRectangle(5, XX - 1, yy - 1, WIDTH + 2, height + 2);
				AddLayer(dialog, XX, yy);
				
				return true;
			}
			return false;
		}
		
		public override bool KeyDown(KeyboardEventArgs args)
		{
			HandleClose();
			Destroy();
			return true;
		}
		
		public override bool MouseDown(ScreenEventArgs args)
		{
			if (args.X >= XX && args.X < (XX + WIDTH))
			{
				int height = (16 * _units.Length) + 6;
				int yy = (200 - height) / 2;
				if (args.Y >= yy && args.Y < (yy + height))
				{
					int y = (args.Y - yy - 3);
					int uid = (y - (y % 16)) / 16;
					if (uid < 0 || uid >= _units.Length)
						return true;
					
					Game.ActiveUnit = _units[uid];
					_units[uid].Busy = false;
					return true;
				}
			}

			HandleClose();
			Destroy();
			return true;
		}

		internal UnitStack(int x, int y)
		{
			_x = x;
			_y = y;
			_units = Map[_x, _y].Units.Take(12).ToArray();

			Cursor = MouseCursor.Pointer;
			
			_canvas = new Picture(320, 200, Common.TopScreen.Palette);
		}
	}
}