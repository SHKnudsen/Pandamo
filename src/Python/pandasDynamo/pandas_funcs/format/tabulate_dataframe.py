from tabulate import tabulate

def tabulate_dataframe(df):
    headers = 'keys'
    tableformat = 'orgtbl'
    tabulated_df = tabulate(df, headers=headers, tablefmt=tableformat)
    return tabulated_df
