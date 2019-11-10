import json
from ast import literal_eval
import pandas as pd
import re
import sys
import os
from flask import Blueprint
from flask import jsonify
from flask import request
from flask import current_app as app
import numpy as np
from utillities.exceptions import ExceptionHelpers

mod = Blueprint('combine_dataframe', __name__)
null = None

@mod.route('merge/', methods=["POST"])
def merge():
    try:
        request_dict = request.get_json()
        left_df = request_dict['leftDf']
        right_df = request_dict['rightDf']
        how = request_dict['how']
        left_on = request_dict['left_on'] 
        right_on = request_dict['right_on']
        left_index = request_dict['left_index'] 
        right_index = request_dict['right_index']
        left_df = pd.read_json(json.dumps(eval(left_df)), orient='split')
        right_df = pd.read_json(json.dumps(eval(right_df)), orient='split')
        df = left_df.merge(right_df, how=how, left_on=left_on, right_on=right_on, left_index=left_index, right_index=right_index)
        df_json = df.to_json(orient='split')
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception = ExceptionHelpers.format_exception(sys.exc_info())
        response = app.response_class(
            response=exception,
            status=400,
            mimetype='application/json'
        )
    return response

@mod.route('concatenate/', methods=["POST"])
def concatenate():
    try:
        request_dict = request.get_json()
        df_json_list = request_dict['df_json_list']
        axis = request_dict['axis']
        join = request_dict['join']
        ignore_index = request_dict['ignore_index']
        df_list = []
        for i in df_json_list:
            df_list.append(pd.read_json(json.dumps(eval(i)), orient='split'))
        df = pd.concat(df_list, axis=axis, ignore_index=ignore_index)
        df_json = df.to_json(orient='split')
        response = app.response_class(
            response=df_json,
            status=200,
            mimetype='application/json'
        )
    except:
        exception = ExceptionHelpers.format_exception(sys.exc_info())
        response = app.response_class(
            response=exception,
            status=400,
            mimetype='application/json'
        )
    return response