import sys
import json
from ast import literal_eval
import pandas as pd
import re

def melt_dataframe(df, id_var, value_var):
    try:
        df = pd.melt(df, id_vars=id_var, value_vars=value_var)
        return df
    except Exception as e:
        return e

df = sys.argv[1]
id_var = sys.argv[2]
value_var = sys.argv[3]

df = melt_dataframe(df,id_var,value_var)
print(df)