import pandas as pd
from tabulate import tabulate
import sys
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from utillities.string_helpers import *

string = "[this,that]"
test = string_to_list(string)
print(test)