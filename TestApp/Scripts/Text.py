import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

this.AddEventHandler('button', EventTypes.Frames.ElementClick, this.ToggleVisibility)

