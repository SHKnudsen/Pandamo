import sys
import json
from ast import literal_eval
import pandas as pd
import re

def drop_columns(df, columns_to_drop):
    try:
        df = df.drop(columns=columns_to_drop)
        return df
    except Exception as e:
        return e

df = sys.argv[1]
columns_to_drop = sys.argv[2]

df = drop_columns(df,columns_to_drop)
print(df)