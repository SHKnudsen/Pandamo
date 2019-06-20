import sys
from tabulate import tabulate
sys.path.append('C:/Users/SylvesterKnudsen/Desktop/pandasDynamo')

def tabulate_dataframe(df):
    headers = 'keys'
    tableformat = 'orgtbl'
    tabulated_df = tabulate(df, headers=headers, tablefmt=tableformat)
    return tabulated_df
