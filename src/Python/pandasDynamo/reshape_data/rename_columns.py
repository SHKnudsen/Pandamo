import sys
import json
from ast import literal_eval
import pandas as pd
import re

def rename_columns(df, oldValue, newValue):
    try:
        if type(oldValue) is list and type(newValue) is list:
            values = dict(zip(oldValue,newValue))
            df = df.rename(index=str, columns=values)
            return df
        elif type(oldValue) is not list and type(newValue) is not list:
            values = {oldValue:newValue}
            df = df.rename(index=str, columns=values)
            return df
        raise Exception('OldValue and NewValue needs to be two equal sized lists or two single items')
    except Exception as e:
        return e

df = sys.argv[1]
oldValue = sys.argv[2]
newValue = sys.argv[3]

df = rename_columns(df,oldValue,newValue)
print(df)