from tabulate import tabulate

def tabular(df):
    headers = 'keys'
    tableformat = 'orgtbl'
    tabulated_df = tabulate(df, headers=headers, tablefmt=tableformat)
    return tabulated_df
