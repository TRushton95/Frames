import clr
clr.AddReference("Frames")
from Frames.EventSystem import *
from Frames.Enums import *
from Frames.DataStructures import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

HIDE_TRANSITION_DURATION = 2000;
SHOW_TRANSITION_DURATION = 250;

def showElement(e):
    this.Show()

def hideElement(e):
    this.Hide(HIDE_TRANSITION_DURATION)

def moveElement(e):
    position = Vector2(this.X, this.Y + 5)
    this.Move(position)

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, showElement)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, hideElement)

this.AddEventHandler('shift-button', EventTypeConstants.Frames.ElementClick, moveElement)

this.AddMovementTransition(TransitionHook.OnShow, PositionProfile(HorizontalAlign.Left, VerticalAlign.Top, 0, 0), SHOW_TRANSITION_DURATION);
this.AddMovementTransition(TransitionHook.OnHide, PositionProfile(HorizontalAlign.Right, VerticalAlign.Middle, 0, 0), HIDE_TRANSITION_DURATION);