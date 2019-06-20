# import flask microframework library
from flask import Flask
from flask import jsonify
from flask import request
import json
import sys
sys.path.append('C:/Users/SylvesterKnudsen/Documents/GitHub/Pandamo/src/Python/pandasDynamo')
from pandas_funcs.create_dataframe.from_converted_dyn_dict import *
from pandas_funcs.format.tabulate_dataframe import *
 
# initialize the flask application
app = Flask(__name__)
 
# endpoint api_1() with post method for create dataframe from dict
@app.route("/api/v1.0/create_dataframe_from_dict", methods=["POST"])
def create_dataframe():
    try:
        request_json = request.get_json()
        df = from_converted_dyn_dict(request_json)
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

# endpoint api_1() with post method for create dataframe from dict
@app.route("/api/v1.0/tabulate_dataframe", methods=["POST"])
def format_tabulate_dataframe():
    try:
        request_json = request.get_json()
        df = pd.read_json(json.dumps(request_json), orient='index')
        df_string = tabulate_dataframe(df)
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


if __name__ == "__main__":
#     run flask application in debug mode
    app.run(debug=True)