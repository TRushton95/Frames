import clr, time, threading
clr.AddReference("Frames")
from Frames.EventSystem import *
from Frames.Enums import *
from Frames.DataStructures import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

def hideMenu():
    this.Hide()

def onMenuOpen(e):
    this.Show()
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Middle, VerticalAlign.Middle, 0, 0))

def onMenuClose(e):
    this.AddMovementTransition(PositionProfile(HorizontalAlign.Middle, VerticalAlign.OffBottom, 0, 0), hideMenu)

def moveElement(e):
    position = Vector2(this.X, this.Y + 5)
    this.Move(position)

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, onMenuOpen)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, onMenuClose)
this.AddEventHandler('shift-button', EventTypeConstants.Frames.ElementClick, moveElement)