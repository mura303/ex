#!ruby

def yield3times
  yield
  yield
  yield
end

yield3times { puts "hoge" }
