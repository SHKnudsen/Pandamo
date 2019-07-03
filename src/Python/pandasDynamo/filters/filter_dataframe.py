import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from deserialize.from_json import dataframe_from_json

def filter_dataframe(df,items, axis):
    try:
        df = df.filter(items=items, axis=axis)
        return df
    except Exception as e:
        return e


df = dataframe_from_json(sys.argv[1])
items = sys.argv[2]
axis = int(sys.argv[3])
if axis == 0:
    items = [int(x) for x in items.split(",")]
else:
    items = [x for x in items.split(",")]
df = filter_dataframe(df,items,axis)
print(df)
