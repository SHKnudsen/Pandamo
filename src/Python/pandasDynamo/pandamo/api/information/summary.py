import sys
import json
from ast import literal_eval
import pandas as pd
import re
from flask import Blueprint
from flask import current_app as app
from flask import request


mod = Blueprint('summary', __name__)
null = None

# Sum of values
@mod.route('sum/', methods=["POST"])
def sum():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        axis = request_dict['axis']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_sum = df.sum(axis = axis, skipna = True)
        values = dataframe_sum.values.tolist()
        keys = dataframe_sum.index.values.tolist()
        sum_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(sum_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Cumulative sum of values
@mod.route('cumulative_sum/', methods=["POST"])
def cumulative_sum():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_cumsum = df.cumsum()
        values = list(map(list, zip(*dataframe_cumsum.values.tolist())))         
        keys = dataframe_cumsum.columns.values.tolist()
        cum_dict = dict(zip(keys, values))      
        response = app.response_class(
            response=json.dumps(cum_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# maximum values
@mod.route('max_value/', methods=["POST"])
def max_value():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_max_value = df.max()
        values = dataframe_max_value.values.tolist()
        keys = dataframe_max_value.index.values.tolist()
        max_value_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(max_value_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Minimum values
@mod.route('min_value/', methods=["POST"])
def min_value():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_min_value = df.min()
        values = dataframe_min_value.values.tolist()
        keys = dataframe_min_value.index.values.tolist()
        min_value_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(min_value_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Maximum index value
# function returns index of first occurrence of maximum over requested axis. 
# While finding the index of the maximum value across any index, all NA/null values are excluded.
@mod.route('max_index_value/', methods=["POST"])
def max_index_value():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        axis = request_dict['axis']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = df.select_dtypes(exclude=['object'])
        dataframe_max_index_value = df.idxmax(axis=axis, skipna=True)
        values = dataframe_max_index_value.values.tolist()
        keys = dataframe_max_index_value.index.values.tolist()
        max_index_value_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(max_index_value_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Minimum index value
@mod.route('min_index_value/', methods=["POST"])
def min_index_value():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        axis = request_dict['axis']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        df = df.select_dtypes(exclude=['object'])
        dataframe_min_index_value = df.idxmin(axis=axis, skipna=True)
        values = dataframe_min_index_value.values.tolist()
        keys = dataframe_min_index_value.index.values.tolist()
        min_index_value_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(min_index_value_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Summary statistics
# basic statistical details like percentile, mean, std etc. of a data frame or a series of numeric values.
@mod.route('describe/', methods=["POST"])
def describe():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_describe = df.describe(include='all')
        df_json = dataframe_describe.to_json(orient='split', date_format='iso')
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

# Mean of values
@mod.route('mean/', methods=["POST"])
def mean():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_mean = df.mean()
        values = dataframe_mean.values.tolist()
        keys = dataframe_mean.index.values.tolist()
        mean_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(mean_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# Median of values
@mod.route('median/', methods=["POST"])
def median():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_median = df.median()
        values = dataframe_median.values.tolist()
        keys = dataframe_median.index.values.tolist()
        median_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(median_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# unique value for each variable
@mod.route('unique/', methods=["POST"])
def unique():
    try:
        request_dict = request.get_json()
        jsonstr = request_dict['jsonStr']
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='split')
        dataframe_nunique = df.nunique()
        values = dataframe_nunique.values.tolist()
        keys = dataframe_nunique.index.values.tolist()
        nunique_dict = dict(zip(keys, values))
        response = app.response_class(
            response=json.dumps(nunique_dict),
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response