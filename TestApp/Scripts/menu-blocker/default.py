import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

def showElement(e):
    this.Show()

def hideElement(e):
    this.Hide()

this.AddEventHandler('menu', EventTypeConstants.Frames.ElementShow, showElement)
this.AddEventHandler('menu', EventTypeConstants.Frames.ElementHide, hideElement)
