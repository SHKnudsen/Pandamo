import pandas as pd
from tabulate import tabulate
import sys
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from utillities.string_helpers import *

string = "[this,that]"
test = string_to_list(string)

# save filepath to variable for easier access
melbourne_file_path = 'C:/Users/SylvesterKnudsen/Desktop/melb-data/melb_data.xlsx'
# read the data and store data in DataFrame titled melbourne_data
melbourne_data = pd.read_excel(melbourne_file_path, sheet_name='melb_data', header=0, na_values=['', ' '])
# print a summary of the data in Melbourne data
df = pd.to_datetime(melbourne_data['Date'])
print(df.to_json(orient='split', date_unit='ns'))