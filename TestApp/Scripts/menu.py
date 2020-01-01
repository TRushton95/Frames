import clr, time, threading
clr.AddReference("Frames")
from Frames.EventSystem import *
from Frames.Enums import *
from Frames.DataStructures import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

HIDE_TRANSITION_DURATION = 2000;
SHOW_TRANSITION_DURATION = 250;

def onShow(e):
    this.Show();
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Left, VerticalAlign.Top, 0, 0), SHOW_TRANSITION_DURATION);

def onHide(e):
    threading.Timer(HIDE_TRANSITION_DURATION/1000, this.Hide).start();
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Right, VerticalAlign.Middle, 0, 0), HIDE_TRANSITION_DURATION);

def moveElement(e):
    position = Vector2(this.X, this.Y + 5)
    this.Move(position)

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, onShow)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, onHide)
this.AddEventHandler('shift-button', EventTypeConstants.Frames.ElementClick, moveElement)