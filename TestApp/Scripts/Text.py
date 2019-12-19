import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

this.AddEventHandler('button', EventTypeConstants.Frames.ElementClick, this.ToggleVisibility)

