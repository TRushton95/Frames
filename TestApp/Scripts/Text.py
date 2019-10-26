import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

this.AddEventHandler(EventTypes.Frames.ElementClick, this.ToggleVisibility)

