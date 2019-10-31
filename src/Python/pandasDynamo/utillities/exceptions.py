
class ExceptionHelpers:

    @staticmethod
    def format_exception(exception_obj):
        exception_type = str(exception_obj[0].__name__)
        exception_message = exception_obj[1]
        exception_response = f"Error Type: {exception_type}\nException: {exception_message}"
        return exception_response

