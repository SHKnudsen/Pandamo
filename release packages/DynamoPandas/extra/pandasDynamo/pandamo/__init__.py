from flask import Flask
from config import Config
from pandamo import api
import os

# Disable Intel Fortran default console event handler
# https://github.com/ContinuumIO/anaconda-issues/issues/905
env = 'FOR_DISABLE_CONSOLE_CTRL_HANDLER'
if env not in os.environ:
    os.environ[env] = '1'

app = Flask(__name__)

from pandamo.api.server.has_server_started import mod

from pandamo.api.dataframes.create_dataframe import mod
from pandamo.api.filters.filter_dataframe import mod
from pandamo.api.format.format_dataframe import mod
from pandamo.api.reshape.reshape_dataframe import mod
from pandamo.api.select.select_rows import mod
from pandamo.api.select.select_columns import mod
from pandamo.api.information.basic_information import mod
from pandamo.api.information.summary import mod
from pandamo.api.combine.combine_dataframe import mod
from pandamo.api.statistics.stats import mod

app.register_blueprint(api.server.has_server_started.mod, url_prefix='/api/server')

app.register_blueprint(api.dataframes.create_dataframe.mod, url_prefix='/api/create_dataframe')
app.register_blueprint(api.filters.filter_dataframe.mod, url_prefix='/api/filter_dataframe')
app.register_blueprint(api.format.format_dataframe.mod, url_prefix='/api/format_dataframe')
app.register_blueprint(api.reshape.reshape_dataframe.mod, url_prefix='/api/reshape_dataframe')
app.register_blueprint(api.select.select_rows.mod, url_prefix='/api/select_rows')
app.register_blueprint(api.select.select_columns.mod, url_prefix='/api/select_columns')
app.register_blueprint(api.information.basic_information.mod, url_prefix='/api/basic_information')
app.register_blueprint(api.information.summary.mod, url_prefix='/api/summary')
app.register_blueprint(api.combine.combine_dataframe.mod, url_prefix='/api/combine')
app.register_blueprint(api.statistics.stats.mod, url_prefix='/api/statistics')

from pandamo.api.error_handlers import blueprint

app.register_blueprint(api.error_handlers.blueprint)