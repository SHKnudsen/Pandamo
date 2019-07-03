import sys
import json
from ast import literal_eval
import pandas as pd
import re

def sort_values(df, column, ascending=True):
    try:
        df = df.sort_values(ascending=ascending, by=column)
        return df
    except Exception as e:
        return e

df = sys.argv[1]
column = sys.argv[2]
ascending = sys.argv[3]

df = sort_values(df,column,ascending)
print(df)