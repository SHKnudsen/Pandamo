import sys
import json
from ast import literal_eval
import pandas as pd
import re

def pivot_dataframe(df, index, columns, values):
    try:
        df = df.pivot(index=index,columns=columns, values=values)
        return df
    except Exception as e:
        return e
df = sys.argv[1]
index = sys.argv[2]
columns = sys.argv[3]
values = sys.arg[4]

df = pivot_dataframe(df,index,columns,values)
print(df)