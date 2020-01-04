import clr, time, threading
clr.AddReference("Frames")
from Frames.EventSystem import *
from Frames.Enums import *
from Frames.DataStructures import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

MENU_TRANSITION_DURATION_MS = 1000;
MENU_TRANSITION_DURATION_S = float(MENU_TRANSITION_DURATION_MS) / 1000

def onMenuOpen(e):
    this.Show()
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Middle, VerticalAlign.Middle, 0, 0), MENU_TRANSITION_DURATION_MS)

def onMenuClose(e):
    threading.Timer(MENU_TRANSITION_DURATION_S, this.Hide).start()
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Middle, VerticalAlign.OffBottom, 0, 0), MENU_TRANSITION_DURATION_MS)

def moveElement(e):
    position = Vector2(this.X, this.Y + 5)
    this.Move(position)

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, onMenuOpen)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, onMenuClose)
this.AddEventHandler('shift-button', EventTypeConstants.Frames.ElementClick, moveElement)