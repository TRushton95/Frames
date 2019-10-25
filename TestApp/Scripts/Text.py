import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

this.AddEventHandler(EventTypes.Frames.ElementHover, this.Show)
this.AddEventHandler(EventTypes.Frames.ElementHoverLeave, this.Hide)