# import flask microframework library
from flask import Flask
from flask import request
import json
import sys
import pandas as pd
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from pandas_funcs.dataframes import create_dataframe
from pandas_funcs.format import dataframe_formatters
from utillities.string_helpers import string_to_list
from pandas_funcs.filters import filter_dataframe
 
# initialize the flask application
app = Flask(__name__)

# endpoint api_1() with post method for create dataframe from dict
@app.route("/pandamo/create_dataframe_from_dict/<string:jsonstring>")
def new_dataframe(jsonstring):
    try:
        df = create_dataframe.by_dict(eval(jsonstring))
        df_json = df.to_json(orient='index')
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

# endpoint api_1() with post method for tabulateing dataframe
@app.route("/pandamo/tabulate_dataframe/<string:jsonstr>")
def format_tabulate_dataframe(jsonstr):
    try:
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
        df_string = dataframe_formatters.tabular(df)
        response = app.response_class(
            response=df_string,
            status=200,
            mimetype='application/json'
        )
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response

# endpoint api_1() with post method for filtering dataframe
@app.route("/pandamo/filter_dataframe/<string:jsonstr>/<string:items>/<int:axis>")
def filters_filter_dataframe(jsonstr,items,axis):
    try:
        items = string_to_list(items)
        axis = int(axis)
        df = pd.read_json(json.dumps(eval(jsonstr)), orient='index')
        filtered_df = filter_dataframe.by_items(df,items,axis)
        df_json = filtered_df.to_json(orient='index')
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


if __name__ == "__main__":
#     run flask application in debug mode
    app.run(debug=True)