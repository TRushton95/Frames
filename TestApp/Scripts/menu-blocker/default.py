import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

def showElement(e):
    this.Show()

def hideElement(e):
    this.Hide()

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, showElement)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, hideElement)
