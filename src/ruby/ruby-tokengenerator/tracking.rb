# frozen_string_literal: true

class Tracking

  def initialize(trackingId)
    @trackingId = trackingId
  end

  def to_hash
    hash = {}
    instance_variables.each { |var| hash[var.to_s.delete('@')] = instance_variable_get(var) }
    hash
  end
end
