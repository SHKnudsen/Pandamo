import sys
import io
import json
from ast import literal_eval
import pandas as pd
import numpy as np
import re
from flask import Blueprint
from flask import current_app as app
from flask import request


mod = Blueprint('basic_information', __name__)
null = None

# (rows,columns)
@mod.route('shape/', methods=["POST"])
def shape():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_shape = df.shape
        response = app.response_class(
            response=json.dumps(dataframe_shape),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Describe index
@mod.route('index/', methods=["POST"])
def index():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_index = {"index":df.index.values.tolist(),"dtype":str(df.index.dtype)}
        response = app.response_class(
            response=json.dumps(dataframe_index),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Describe DataFrame columns
@mod.route('columns/', methods=["POST"])
def columns():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_columns = df.columns
        columns_dict = {"columns":dataframe_columns.values.tolist(),"dtype":str(dataframe_columns.dtype)}
        response = app.response_class(
            response=json.dumps(columns_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Print a concise summary of a DataFrame.
# This method prints information about a DataFrame including the index 
# dtype and column dtypes, non-null values and memory usage.
@mod.route('info/', methods=["POST"])
def info():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        buf = io.StringIO()
        dataframe_info = df.info(buf=buf)
        response = app.response_class(
            response=buf.getvalue(),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

#------------ needs testing (remember json.dumps response!!) -----------------------
# Number of non-NA values
@mod.route('count/', methods=["POST"])
def count():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        from scipy import stats
        dataframe_count = df.count()
        count_dict = dict(zip(dataframe_count.index.values.tolist(), dataframe_count.values.tolist()))
        response = app.response_class(
            response=json.dumps(count_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Datatypes
@mod.route('datatypes/', methods=["POST"])
def datatypes():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_datatypes = df.dtypes
        values = [val.name for val in dataframe_datatypes.values.tolist()]
        keys = dataframe_datatypes.index.values.tolist()
        datatypes_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(datatypes_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response
