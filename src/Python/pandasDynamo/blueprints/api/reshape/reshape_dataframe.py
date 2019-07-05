import sys
import json
from ast import literal_eval
import pandas as pd
import re
from flask import Blueprint
from flask import current_app as app
from utillities.string_helpers import string_to_list

mod = Blueprint('reshape_dataframe', __name__)

@mod.route('sort_values/<string:jsonstr>/<string:columns>/<string:ascending>')
def sort_values(jsonstr, columns, ascending):
    try:
        columns = string_to_list(columns)
        asc = ascending == "True"
        df = pd.DataFrame(eval(jsonstring))
        df = df.sort_values(ascending=asc, by=column)
        df_json = df.to_json(orient='split')
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

@mod.route('rename_columns/<string:jsonstr>/<string:oldValue>/<string:newValue>')
def rename_columns(df, oldValue, newValue):
    try:
        oldValue = string_to_list(oldValue)
        newValue = string_to_list(newValue)
        if(len(oldValue != len(newValue))):
            raise Exception('OldValue and NewValue needs to be two equal sized lists or two single items')
        values = dict(zip(oldValue,newValue))
        df = pd.DataFrame(eval(jsonstring))
        df = df.rename(index=str, columns=values)
        df_json = df.to_json(orient='split')
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

'''

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

'''