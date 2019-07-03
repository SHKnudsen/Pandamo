import sys
from tabulate import tabulate
sys.path.append('C:/Users/SylvesterKnudsen/Desktop/pandasDynamo')
from deserialize.from_json import dataframe_from_json

def tabulate_dataframe(df):
    headers = 'keys'
    tableformat = 'orgtbl'
    tabulated_df = tabulate(df, headers=headers, tablefmt=tableformat)
    return tabulated_df

df = dataframe_from_json(sys.argv[1])  
print(tabulate_dataframe(df))