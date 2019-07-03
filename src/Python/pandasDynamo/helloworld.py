import sys
import json
from ast import literal_eval
import pandas as pd
import re

# Create Dataframes
def create_dataframe(data_string):
    try:
        split_str = data_string
        keys = re.search('Keys:(.+?),Values',split_str).group(1)[1:-1].split(',')
        values = re.search('Values:(.+?),Count',split_str).group(1)[2:-2].split("],[")
        values = [i.split(",") for i in values]
        python_dictionary = dict(zip(keys,values))
        dataframe = pd.DataFrame(python_dictionary)
        return dataframe
    except Exception as e:
        return e

# Serialize dataframe
def dataframe_to_json(df):
    try:
        jsonstr = df.to_json()
        return jsonstr
    except Exception as e:
        return e

# Deserialize dataframe
def dataframe_from_json(json_str):
    try:
        dataframe = pd.read_json(json_str)
        dataframe = dataframe.reset_index(drop=True)
        return dataframe
    except Exception as e:
        return e

# Reshape Data
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

def drop_columns(df, columns_to_drop):
    try:
        df = df.drop(columns=columns_to_drop)
        return df
    except Exception as e:
        return e

def drop_rows(df,index_to_drop):
    try:
        df = df.drop(index_to_drop, axis=0)
        return df
    except Exception as e:
        return e

# filter
def filter_dataframe(df,items, axis):
    try:
        df = df.filter(items=items, axis=axis)
        return df
    except Exception as e:
        return e
    
def filter_dataframe_by_regex(df,items, axis):
    try:
        df = df.filter(regex=items, axis=axis)
        return df
    except Exception as e:
        return e

def filter_dataframe_by_contains(df,items, axis):
    try:
        df = df.filter(like=items, axis=axis)
        return df
    except Exception as e:
        return e

df = create_dataframe(sys.argv[1][1:-1])
#jsonstr = dataframe_to_json(df)
#dataframe = dataframe_from_json(jsonstr)
#dataframe = pivot_dataframe(dataframe, "two's", "one's", "three's")
#dataframe = melt_dataframe(dataframe,"two's", "three's")
#dataframe = sort_values(dataframe,"three's", False)
#dataframe = rename_columns(dataframe,"three's","newValue")
#dataframe = drop_columns(dataframe, "newValue")
#dataframe = drop_rows(dataframe, 1)

#datastr = """{Keys:[one's,three's,two's],Values:[[1,11,111],[3,33,333],[2,22,222]],Count:3}"""
#df = create_dataframe(datastr)
#jsonstr = dataframe_to_json(df)
#df = dataframe_from_json(jsonstr)
#dataframe = dataframe.reset_index()
#dataframe = dataframe.pivot(index=3,columns=2, values=1)
#dataframe = rename(dataframe, [1,2], ["one","two"])
#dataframe = drop_rows(dataframe,1)
#df = filter_dataframe(df, [1,2], 0)
#df = filter_dataframe_by_regex(df,"e's$",1)
#df = filter_dataframe_by_contains(df,"ee",1)

print(df)
