import sys
import json
from ast import literal_eval
import pandas as pd
import re

def drop_rows(df,index_to_drop):
    try:
        df = df.drop(index_to_drop, axis=0)
        return df
    except Exception as e:
        return e

df = sys.argv[1]
index_to_drop = sys.argv[2]

df = drop_rows(df,index_to_drop)
print(df)