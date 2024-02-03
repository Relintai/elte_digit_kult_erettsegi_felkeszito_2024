from dataclasses import dataclass
from datetime import datetime
import string 

@dataclass
class szenzor:
    dátum: datetime
    kód: string
    mérés: float
