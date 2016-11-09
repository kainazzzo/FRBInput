using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using RacingController.Screens;
namespace RacingController.Screens
{
	public partial class GameController
	{
        void OnGasButtonRollOn (FlatRedBall.Gui.IWindow callingWindow)
        {
            FlatRedBall.Debugging.Debugger.CommandLineWrite("Down");
        }
        void OnGasButtonRollOff (FlatRedBall.Gui.IWindow callingWindow)
        {
            FlatRedBall.Debugging.Debugger.CommandLineWrite("Up");
        }
		
	}
}
