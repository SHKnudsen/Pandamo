from flask import Flask

app = Flask(__name__)

from blueprints.api.dataframes.create_dataframe import mod
from blueprints.api.filters.filter_dataframe import mod
from blueprints.api.format.format_dataframe import mod

app.register_blueprint(api.dataframes.create_dataframe.mod, url_prefix='/api/create_dataframe')
app.register_blueprint(api.filters.filter_dataframe.mod, url_prefix='/api/filter_dataframe')
app.register_blueprint(api.format.format_dataframe.mod, url_prefix='/api/format_dataframe')