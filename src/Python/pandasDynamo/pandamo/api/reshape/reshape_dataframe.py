import sys
import json
from ast import literal_eval
import pandas as pd
import re
from flask import Blueprint
from flask import current_app as app
from flask import request


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
        df_json = df.to_json(orient='split', date_format='iso')
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
        df_json = df.to_json(orient='split', date_format='iso')
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
        df_json = df.to_json(orient='split', date_format='iso')
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
        df_json = df.to_json(orient='split', date_format='iso')
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
        df_json = df.to_json(orient='split', date_format='iso')
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
        df_json = df.to_json(orient='split', date_format='iso')
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

@mod.route('reverse_dataframe/', methods=["POST"])
def reverse_dataframe():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        axis = request_dict['axis']
        reset_index = request_dict['reset_index']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')      
        # axis 1 columns
        if(axis == 1):     
            df = df.loc[:, ::-1].reset_index(drop=reset_index)
        # axis 0 rows
        elif(axis == 0):
            df = df.loc[::-1].reset_index(drop=reset_index)
        else:
            raise Exception
        df_json = df.to_json(orient='split', date_format='iso')
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

# Consider if this is needed
@mod.route('strings_to_numbers/', methods=["POST"])
def strings_to_numbers():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        strings = request_dict['strings']
        number_types = request_dict['numberTypes']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')   
        values = dict(zip(strings,number_types))    
        df = df.astype(values)
        df_json = df.to_json(orient='split', date_format='iso')
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

@mod.route('fill_na/', methods=["POST"])
def fill_na():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')    
        df = df.fillna(method='ffill')
        df_json = df.to_json(orient='split', date_format='iso')
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

@mod.route('to_datetime/', methods=["POST"])
def to_datetime():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        column = request_dict['column']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')    
        df[column] = pd.to_datetime(df[column])
        df_json = df.to_json(orient='split', date_format='iso')
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

@mod.route('drop_na/', methods=["POST"])
def drop_na():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')    
        df = df.dropna()
        df_json = df.to_json(orient='split', date_format='iso')
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

@mod.route('get_dummies/', methods=["POST"])
def get_dummies():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')         
        df = pd.get_dummies(df)
        df_json = df.to_json(orient='split', date_format='iso')
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