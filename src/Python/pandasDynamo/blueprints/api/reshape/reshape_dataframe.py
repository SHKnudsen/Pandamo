import sys
import json
from ast import literal_eval
import pandas as pd
import re
from flask import Blueprint
from flask import current_app as app
from flask import request
from utillities.string_helpers import string_to_list

mod = Blueprint('reshape_dataframe', __name__)
null = None
@mod.route('sort_values/', methods=["POST"])
def sort_values():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        columns = request_dict['columns']
        ascending = request_dict['ascending']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = df.sort_values(ascending=ascending, by=columns)
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

@mod.route('rename_columns/', methods=["POST"])
def rename_columns():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        old_value = request_dict['old_value']
        new_value = request_dict['new_value']
        if(len(old_value) != len(new_value)):
            raise Exception('OldValue and NewValue needs to be two equal sized lists or two single items')
        values = dict(zip(old_value,new_value))
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
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

@mod.route('pivot_dataframe/', methods=["POST"])
def pivot_dataframe():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        index = request_dict['index']
        columns = request_dict['columns']
        values = request_dict['values']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = df.pivot(index=index,columns=columns, values=values)
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

@mod.route('melt_dataframe/', methods=["POST"])
def melt_dataframe():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        id_var = request_dict['id_var']
        value_var = request_dict['value_var']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = pd.melt(df, id_vars=id_var, value_vars=value_var)
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

@mod.route('drop_rows/', methods=["POST"])
def drop_rows():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        index_to_drop = request_dict['indexToDrop']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = df.drop(index_to_drop, axis=0)
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

@mod.route('drop_columns/', methods=["POST"])
def drop_columns():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        columns_to_drop = request_dict['columnsToDrop']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')       
        df = df.drop(columns=columns_to_drop)
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
