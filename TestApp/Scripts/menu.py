import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

def showElement(e):
    this.Show()

def hideElement(e):
    this.Hide()

def moveElement(e):
    position = Vector2(this.X, this.Y + 5)
    this.Move(position)

this.AddEventHandler('shift-button', EventTypeConstants.Frames.ElementClick, moveElement)