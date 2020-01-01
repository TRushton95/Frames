import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

def toggleElementVisibility(e):
    this.ToggleVisibility()

this.AddEventHandler('button', EventTypeConstants.Frames.ElementClick, toggleElementVisibility)

