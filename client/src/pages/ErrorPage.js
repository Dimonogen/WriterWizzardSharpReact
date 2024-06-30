const ErrorPage = ({error}) => {
  return (
      <div className='MContent'>
          {
              error === null || error === undefined?
              'Error Page not found'
                  :
                  error}
      </div>
  )
}

export default ErrorPage;