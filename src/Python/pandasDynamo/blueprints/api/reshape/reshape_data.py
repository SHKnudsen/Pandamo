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

def pivot_dataframe(df, index, columns, values):
    try:
        df = df.pivot(index=index,columns=columns, values=values)
        return df
    except Exception as e:
        return e

def melt_dataframe(df, id_var, value_var):
    try:
        df = pd.melt(df, id_vars=id_var, value_vars=value_var)
        return df
    except Exception as e:
        return e

def drop_rows(df,index_to_drop):
    try:
        df = df.drop(index_to_drop, axis=0)
        return df
    except Exception as e:
        return e

def drop_columns(df, columns_to_drop):
    try:
        df = df.drop(columns=columns_to_drop)
        return df
    except Exception as e:
        return e