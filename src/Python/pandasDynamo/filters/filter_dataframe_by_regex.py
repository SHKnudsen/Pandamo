import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os
sys.path.append('C:/Users/SylvesterKnudsen/Desktop/pandasDynamo')
from deserialize.from_json import dataframe_from_json

def filter_dataframe_by_regex(df,items, axis):
    try:
        df = df.filter(regex=items, axis=axis)
        return df
    except Exception as e:
        return e

df = sys.argv[1]
items = sys.argv[2]
axis = sys.argv[3]

df = filter_dataframe_by_regex(df,items,axis)
print(df)